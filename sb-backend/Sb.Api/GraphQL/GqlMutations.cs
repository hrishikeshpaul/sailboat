
using Ardalis.GuardClauses;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Sb.Api.Authorization;
using Sb.Data;
using Sb.Data.Models.Mongo;
using Sb.Widgets;

namespace Sb.Api.GraphQL
{
    public class GqlMutations
    {
        public async Task<Boat> UpdateBoatAsync(
            [FromServices] IRepository<Boat> boatRepo,
            string boatId,
            string name,
            string description,
            Banner banner)
        {
            Boat b = await boatRepo.GetByIdAsync(boatId);
            b.Name = name ?? b.Name;
            b.Description = description ?? b.Description;
            b.Banner = banner ?? b.Banner;
            await boatRepo.UpdateAsync(b);
            return b;
        }

        public async Task<IWidget> PutWidgetAsync(
            [FromServices] IRepository<IAnchorWidget> widgetRepo,
            [FromServices] IRepository<Boat> boatRepo,
            [FromServices] IHttpContextAccessor contextAccessor,
            [FromServices] IAuthorizationService authService,
            IAnchorWidget widget)
        {
            Guard.Against.NullOrWhiteSpace(widget.BoatId, nameof(widget.BoatId));
            Boat boat = await boatRepo.GetByIdAsync(widget.BoatId);
            Guard.Against.EntityMissing(boat, nameof(boat));
            var authResult = await authService
                .AuthorizeAsync(contextAccessor.HttpContext.User, boat, AuthorizationPolicies.EditBoatPolicy);
            Guard.Against.Forbidden(authResult);

            IAnchorWidget existingWidget;
            if (string.IsNullOrWhiteSpace(widget.Id))
            {
                return await widgetRepo.InsertAsync(widget);
            }
            else
            {
                existingWidget = await widgetRepo.GetByIdAsync(widget.Id);
                Guard.Against.EntityMissing(existingWidget, nameof(existingWidget));
                await widgetRepo.UpdateAsync(widget);
                return widget;
            }
        }
    }
}
