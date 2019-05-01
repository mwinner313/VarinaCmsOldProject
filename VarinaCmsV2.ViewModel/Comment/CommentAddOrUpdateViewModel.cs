using System;
using System.ComponentModel.DataAnnotations;
using VarinaCmsV2.Common.ValidationAttributes;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.ViewModel.Comment
{
    public class CommentAddOrUpdateViewModel : BaseVeiwModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public Guid TargetId { get; set; }
        public CommentTargetType TargetType { get; set; } = CommentTargetType.Undefined;
        [RequiredIfNotAuthenticated]
        public string Email { get; set; }

    }
}
